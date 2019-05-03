<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Board;
use App\User;
use App\Heart;
use App\File;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Log;

define('LINK', 2);
define('POSTS', 4);
define('RANKING', 4);

class HomeController extends Controller
{
    // public function __construct()
    // {
    //     $this->middleware('auth');
    // }

    // 메인 페이지
    public function index()
    {
        /* 상위 3개 조회수 게시글 */
        if(\Auth::check()){
            $rankings = Board::with(['files:path,name', 'user:id,name',
            'hearts' => function($query){
                $query->select('user_id', 'board_id', 'heart')->where('user_id', \Auth::user()->id);
            }])->orderBy('total_heart', 'desc')->take(RANKING)->get();
        }else{
            $rankings = Board::with(['files:path,name', 'user:id,name'])
            ->orderBy('total_heart', 'desc')->take(RANKING)->get();
        }

        $boards = $this->paginate(1, 'latest');
        $page_link_first = 1;
        $page_link_last = 2*LINK+1;
        $currentPage = 1;

        // return response()->json($rankings, 200, [], JSON_PRETTY_PRINT);
        return view('page.main')
            ->with('rankings', $rankings)
            ->with('boards', $boards)
            ->with('page_link_first', $page_link_first)
            ->with('page_link_last', $page_link_last)
            ->with('current_page', $currentPage)
            ->with('sort', 'latest');
    }

    public function paginate($page, $sort){
        // 게시글 조회
        if(\Auth::check()){
            $boards = Board::with(['files:path,name', 'user:id,name',
            'hearts' => function($query){
                $query->select('user_id', 'board_id', 'heart')->where('user_id', \Auth::user()->id);
            }]);
        }else{
            $boards = Board::with(['files:path,name', 'user:id,name']);
        }

        //페이지네이션 세팅
        $currentPage = $page;
        $max_page = ceil($boards->count()/POSTS);

        if($currentPage < 1){
            $currentPage = 1;
        }elseif($currentPage > $max_page){
            $currentPage = $max_page;
        }
        Log::info('sort: '.$sort);
        $boards = $boards->skip(($currentPage-1)*POSTS)->take(POSTS);

        if(!strcmp($sort, 'hearts')){
            Log::info('추천순');
            $boards = $boards->orderBy('total_heart', 'desc')->get();
        }elseif(!strcmp($sort, 'hits')){
            Log::info('조회순');
            $boards = $boards->orderBy('hits', 'desc')->get();
        }elseif(!strcmp($sort, 'latest')){
            Log::info('최신순');
            $boards = $boards->orderBy('created_at', 'desc')->get();
        }
        return $boards;
    }

    //페이지네이션
    public function pagination(Request $request)
    {
        $current_page = $request->page;

        $boards = $this->paginate($current_page, $request->sort);
        $page_link_first = $request->page-LINK;
        $page_link_last = $request->page+LINK;

        Log::info('pagination ajax data :'. $request->page);

        if(LINK+1 > $current_page || $page_link_first < 1){
            $page_link_first = 1;
            $page_link_last = $page_link_last = 2*LINK+1;
        }elseif($page_link_last > ceil(Board::count()/POSTS)){
            $page_link_last = ceil(Board::count()/POSTS);
            if($page_link_last < $current_page+LINK){
                $page_link_first = -2*LINK+$page_link_last;
            }
        }

        if($current_page < 1){
            $current_page = 1;
        }elseif($current_page > $page_link_last){
            $current_page = $page_link_last;
        }

        // return response()->json(ceil(Board::count()/POSTS), 200, [], JSON_PRETTY_PRINT);
        return view('components.main.pagination')
        ->with('boards', $boards)
        ->with('page_link_first', $page_link_first)
        ->with('page_link_last', $page_link_last)
        ->with('current_page', $current_page)
        ->with('sort', $request->sort);
    }

    // 게시글 상세보기
    public function show($id)
    {
        $board = Board::with('files:path,name')->find($id); // 게시글 상세 내용
        if(isset($board->files[0])){
            $video = $board->files[0]->path.$board->files[0]->name;
        }

        if(\Auth::check()){
            $heart = Heart::where('board_id', $id)->where('user_id', \Auth::user()->id);
            Log::info('user_id: '. \Auth::user()->id);
            Log::info('board hits: '. $board->hits);

            if(!$heart->exists()){
                $heart->create([
                    'board_id' => $id,
                    'user_id' => \Auth::user()->id,
                    'hits' => 1,
                ]);
                $board->update(['hits' => $board->hits+1]);
            }elseif($heart->first()->hits == false){
                $heart->update([
                    'hits' => 1,
                ]);
                $board->update(['hits' => $board->hits+1]);
            }
            Log::info('heart hits: '. $heart->first()->hits);
        }
        // return response()->json($board, 200, [], JSON_PRETTY_PRINT);
        if(isset($board->files[0])){
            return view('page.board')
            ->with('board', $board)
            ->with('video', $video);
        }else{
            return view('page.board')
            ->with('board', $board);
        }
    }

    // 마이 페이지
    public function myPage()
    {
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
    public function userPage($id)
    {
        $boards = Board::all()->where('user_id', $id);
        $user = User::select('name')->find($id);

        // return response()->json($user, 200, [], JSON_PRETTY_PRINT);
        return view('page.userPage')
        ->with('boards', $boards)
        ->with('user', $user);
    }

    // 구동 시간 체크
    function get_time() {
        list($usec, $sec) = explode(" ", microtime());
        return ((float)$usec + (float)$sec);
    }

    // 좋아요 처리
    public function heartToggle(Request $request)
    {
        $start = $this->get_time();

        $heart = Heart::where('user_id', \Auth::user()->id)
                        ->where('board_id', $request->id);

        $total_heart = Board::where('id', $request->id)->select('total_heart')->find($request->id);
        // Log::info('heartToggle check / total_heart : '. Board::where('id', $request->id)->get());

        // Log::info('heartToggle check / $heart : '. $heart->get());
        // Log::info('heartToggle check / $heart->exists() : '. $heart->exists());
        if(!$heart->exists()){
            // Log::info('heartToggle check / !$heart->exists() : '. $heart->exists());
            Heart::create([
                'board_id' => $request->id,
                'user_id' => \Auth::user()->id,
            ]);
        }

        if($heart->where('heart', true)->exists()){
            // Log::info('heartToggle false');
            Heart::where('user_id', \Auth::user()->id)
                        ->where('board_id', $request->id)->update(['heart' => false]);
            Board::where('id', $request->id)->update(['total_heart' => $total_heart->total_heart-1]);
        }else{
            // Log::info('heartToggle true');
            Heart::where('user_id', \Auth::user()->id)
                        ->where('board_id', $request->id)->update(['heart' => true]);
            Board::where('id', $request->id)->update(['total_heart' => $total_heart->total_heart+1]);
        }

        $board = Board::with(['hearts' => function($query){
            $query->select('user_id', 'board_id', 'heart')
            ->where('user_id', \Auth::user()->id);
         }])->find($request->id);

        $end = $this->get_time();
        $time = $end - $start;
        Log::info('heartToggle check / time : '. $time);

        // return response()->json($heart, 200, [], JSON_PRETTY_PRINT);
        return view('components.heart')->with('board', $board);
    }

    // 다운로드 체크
    public function download(Request $request){
        $heart = Heart::where('user_id', \Auth::user()->id)
                        ->where('board_id', $request->id)
                        ->update(['dl_check' => true]);
        return $heart;
    }

    // 게시글 작성 페이지
    public function create(){
        // $heart = Heart::where('user_id', \Auth::user()->id)->where('dl_check', true)->get();
        $files = File::where('user_id', \Auth::user()->id)
        // ->where('type', 'mp4')
        ->where('dl_check', true)->get();

        return view('page.write')
        ->with('files', $files);
    }

    // 게시글 등록
    public function store(Request $request)
    {
        //
    }

    // 게시글 수정 페이지
    public function edit($id)
    {
        //
    }

    // 게시글 수정 페이지
    public function update(Request $request, $id)
    {
        //
    }

    // 게시글 삭제
    public function destroy($id)
    {
        //
    }
}
