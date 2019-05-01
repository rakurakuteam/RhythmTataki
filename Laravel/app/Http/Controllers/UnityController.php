<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Log;
use Illuminate\Support\Facades\Storage;
use Aws\Laravel\AwsFacade;
use Illuminate\Http\Request;
use App\User;
use App\Score;
use App\Song;
use App\File;

class UnityController extends Controller
{
    // 유니티 회원가입
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

    public function loginForm(){
        return view('components.auth.unityAuth');
    }

    // 유니티 로그인
    public function login(Request $request)
    {
        //['email' : email_value, 'pw' : pw_value]
        // 비밀번호 틀린 경우만 false
        Log::info('login email: '.$request->email.' / login pw: '. $request->pw);
        
        $password = User::where('email', $request->email)->value('password');
        $user = User::where('email', $request->email);

        if($user->exists() && Hash::check($request->pw, $password)){
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

    //단일점수
    public function getScore($email, $song){
        $user = User::where('email', $email)->value('id');
        $scores = Score::join('songs', 'scores.song_id', '=', 'songs.id')
        ->select('name', 'score')->where('user_id', $user)->where('name', $song)->get();
        
        // key     :  value
        // 노래제목 :  점수
        $k_v_score = [];
        foreach($scores as $score){
            $k_v_score[$score->name] = $score->score; 
        }

        // return response()->json($k_v_score, 200, [], JSON_PRETTY_PRINT);
        return json_encode($k_v_score);
    }

    //점수들
    public function getScores($email){
        $user = User::where('email', $email)->value('id');
        $scores = Score::join('songs', 'scores.song_id', '=', 'songs.id')
        ->select('name', 'score')->where('user_id', $user)->get();
        
        // key     :  value
        // 노래제목 :  점수
        $k_v_scores = [];
        foreach($scores as $score){
            $k_v_scores[$score->name] = $score->score; 
        }

        // return response()->json($k_v_score, 200, [], JSON_PRETTY_PRINT);
        return json_encode($k_v_scores);
    }

    //파일업로드
    public function fileUpload(Request $request)
    {
        $fileType = $request->file->getClientOriginalExtension();
        $fileName = $request->file->getClientOriginalName().$fileType;
        $path = $request->file('file')->storeAs(
            'test/files', $fileName, 's3'
        );
        $size = Storage::disk('s3')->size($fileName);
        $user = User::where('email', $request->email)->pluck('id')->first();
        $url = Storage::url('test/files/'.$fileName);

        Log::info('file path:'. $path);
        Log::info('file url:'. $url);
        Log::info('file name:'. $request->file->getClientOriginalName());
        Log::info('file type:'. $request->file->getClientOriginalExtension());
        Log::info('file size:'. $request->file->getClientSize());
        Log::info('file size:'. $size);

        File::create([
            'user_id' => $user,
            'path' => $url,
            'name' => $fileName,
            'type' => $fileType,
            'size' => $size,
            'created_at' => now(),
        ]);

        return "업로드 성공";
    }

    // request URL 파일명, 이메일
    // return 파일, 확장자
    public function fileDownload(Request $request){
        Log::info('email: '.$request->email);
        Log::info('fileName: '.$request->fileName);
        $user = User::where('email', $request->email)->pluck('id');
        $fileName = $request->fileName;
        $file = File::where('user_id', $user)->where('name', $fileName);
        $path = $file->pluck('path')->first();

        $s3 = AwsFacade::createClient('s3');
        $result = $s3->getObject([
            'Bucket'    => 'capstone.rhythmtataki.bucket',
            'Key'       => 'files/'.$fileName,
        ]);
        
        $headers = ['Content-Type' => $result['ContentType']];
        // $s3 = Storage::download($url, "somsatang.ogg");
        // return response()->download('capstone.rhythmtataki.bucket/'.$fileName, $fileName, $headers);
        return $result;
    }

    public function getMusicList($email){
        $user = User::where('email', $email)->pluck('id')->first();
        $list = File::where('user_id', $user)->where('dl_check', false)->select('path', 'name')->get();
        return $list;
    }
}
