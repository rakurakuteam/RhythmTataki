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

    //점수 기록
    //최고 기록이면 1 / DB 점수 업데이트
    //아니면 0
    public function setScore(Request $request){
        $user = User::where('email', $request->email)->value('id');
        $song = Song::where('name', $request->song)->value('id');
        $score = Score::where('user_id', $user)->where('song_id', $song);

        if($score->where('score', '<', $request->score)->exists()){
            $score->update(['score' => $request->score,
                            'created_at' => now()]);
            return 1;
        }
        return 0;
    }

    public function getScores($email){
        $user = User::where('email', $email)->value('id');
        $scores = Score::join('songs', 'scores.song_id', '=', 'songs.id')
        ->select('name', 'score')->where('user_id', $user)->get();

        return json_encode($scores);
    }
}
