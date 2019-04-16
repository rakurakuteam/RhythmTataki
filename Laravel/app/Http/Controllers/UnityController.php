<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Log;
use Illuminate\Http\Request;
use App\User;
use App\Score;
use App\Song;

class UnityController extends Controller
{
    public function join(Request $request)
    {
        //['email' : email_value, 'pw' : pw_value, 'nmae' : name_value]
        // 이메일 중복만 false
        Log::info('join email: '.$request->email.' / login pw: '. $request->pw. ' / join name: '.$request->name);
        if(User::where('email', $request->email)->exists()){
            Log::info('이메일 중복');
            return 0;
        }
        $user = User::create([
            'email' => $request->email,
            'password' => Hash::make($request->pw),
            'name' => $request->name,
        ]);

        for($i=1; $i<=Song::count(); $i++){
            Score::create([
                'user_id' => $user->id,
                'song_id' => $i,
                'score' => 0,
                'created_at' => now(),
            ]);
        }
        Log::info('회원가입 성공');
        return 1;
    }

    public function login(Request $request)
    {
        //['email' : email_value, 'pw' : pw_value]
        // 비밀번호 틀린 경우만 false
        Log::info('login email: '.$request->email.' / login pw: '. $request->pw);
        
        $hashedValue = User::where('email', $request->email)->value('password');
        $user = User::where('email', $request->email);

        if($user->exists() && Hash::check($request->pw, $hashedValue)){
            Log::info('로그인 성공');
            $song_count = Song::count();
            $score_count = Score::where('user_id', $user->first()->id)->count();
            if($song_count > $score_count){
                for($i=$score_count+1; $i<=$song_count; $i++){
                    Score::create([
                        'user_id' => $user->first()->id,
                        'song_id' => $i,
                        'score' => 0,
                        'created_at' => now(),
                    ]);
                }
            }
            return 1;
        }
        Log::info('비밀번호가 틀렸습니다.');
        return 0;
    }

    public function setScore(Request $request){

        return response()->json($request->all(), 200, [], JSON_PRETTY_PRINT);
    }

    public function getScore(Request $request){
        $user = User::where('email', $request->email)->value('id');
        $score = Score::with('song:id,name')
        ->where('user_id', $user)->where('song_id', 1)->select('user_id','song_id', 'score')->first();

        return json_encode($score);
    }

    public function getScores(Request $request){
        $user = User::where('email', $request->email)->value('id');
        $scores = Score::with('song:id,name')
        ->where('user_id', $user)->select('user_id','song_id', 'score')->get();
        
        return 1;
    }
}
