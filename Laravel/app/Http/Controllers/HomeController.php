<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Board;
use App\User;
use App\Heart;
use App\File;
use Illuminate\Support\Facades\DB;
class HomeController extends Controller
{
    // public function __construct()
    // {
    //     $this->middleware('auth');
    // }

    public function index(){
        $boards = Board::paginate(9);
        $hearts = Board::has('hearts')->get();
        $hearts2 = Board::doesntHave('hearts')->get();
        $hearts3 = Board::withCount('hearts')->get();
        $hearts4 = Board::with('hearts')->get();
        $hearts5 = Heart::all();

        foreach($hearts4 as $heart){
            echo $heart->hearts->where('heart', true)->count()."<br>";
        }
        // return response()->json($a, 200, [], JSON_PRETTY_PRINT);
        // return view('home')->with('boards', $boards);
    }

    // 게시글 상세보기
    public function show($id){
        $board = Board::find($id); // 게시글 상세 내용
        $heart = $board->hearts->where('heart', true)->count(); // 게시글 추천수

        // return response()->json($board->hearts->where('heart', true)->count(), 200, [], JSON_PRETTY_PRINT);
        return view('page.board')
        ->with('board', $board)
        ->with('heart', $heart);
    }

    // 마이 페이지
    public function myPage(){
        $boards = Board::whereHas('hearts', function ($query) { // hearts가 존재
            $query->where('heart', true)->where('user_id', \Auth::user()->id); // 좋아요 눌렀고, 아이디가 현재 로그인한 사용자인
        })->get();

        $boards->load(['user' => function($query){
            $query->select('id', 'name');
        }]); // users 테이블에서 select

        $myBoards = Board::all()->where('user_id', \Auth::user()->id); // 내 게시글
        
        // return response()->json($myBoards, 200, [], JSON_PRETTY_PRINT);
        return view('page.myPage')
        ->with('boards', $boards)
        ->with('myBoards', $myBoards);
    }

    // 사용자 페이지
    public function userPage($id){
        $boards = Board::all()->where('user_id', $id);
        $user = User::select('name')->find($id);

        // return response()->json($user, 200, [], JSON_PRETTY_PRINT);
        return view('page.userPage')
        ->with('boards', $boards)
        ->with('user', $user);
    }
}