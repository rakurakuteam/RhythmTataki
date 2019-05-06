<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Log;
use Illuminate\Support\Facades\Storage;
use Aws\Laravel\AwsFacade;
use Zip;
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
        // return count($request->file('file'));
        // return $request->file('file')[0];
        $user = User::where('email', $request->email)->pluck('id')->first();

        $count = count($request->file);

        for($i=0; $i<$count; $i++){
            $fileType = $request->file[$i]->getClientOriginalExtension();
            $fileName = $request->file[$i]->getClientOriginalName();

            $path = $request->file('file')[$i]->storeAs(
                'files/'.$request->email."/", $fileName, 's3'
            );

            $url = Storage::disk('s3')->url('files/'.$request->email.'/'.$fileName);
            $size = round($request->file[$i]->getClientSize()/1024/1024, 2);

            Log::info('file path:'. $path);
            Log::info('file url:'. $url);
            Log::info('file name:'. $request->file[$i]->getClientOriginalName());
            Log::info('file type:'. $request->file[$i]->getClientOriginalExtension());
            Log::info('file size:'. $request->file[$i]->getClientSize());
            
            File::create([
                'user_id' => $user,
                'path' => $url,
                'name' => $fileName,
                'type' => $fileType,
                'size' => $size,
                'dl_check' => true,
                'created_at' => now(),
            ]);
        }   
        return "업로드 성공";
    }

    // public function zip_test($i){
        // $s3 = AwsFacade::createClient('s3');
        // $result = $s3->getObject([
        //     'Bucket'    => 'capstone.rhythmtataki.bucket',
        //     'Key'       => 'files/', // 다운로드 할 파일 경로 및 이름
        // ]);

        // $headers = [
        //     'Pragma' => 'public',
        //     'Expires' => 0,
        //     'Content-Type' => $result['ContentType'],
        //     'Content-Disposition' => "attachment; filename=091".$i.".txt", // 다운로드 되는 이름
        //     'Content-Transfer-Encoding' => 'binary',
        //     'Content-Length' => $result['ContentLength']
        // ];
        // return $headers;
        // return $result;
        // ('다운로드 할 파일 경로 및 이름')
    //     return Storage::disk('s3')->download('files/bbb@naver.com/091'.$i.'.txt', 'test.txt', $headers);
    // }

    // request URL 파일명, 이메일
    // return 파일, 확장자
    public function fileDownload(Request $request){
        // Log::info('email: '.$request->email);
        // Log::info('fileName: '.$request->fileName);
        // $user = User::where('email', $request->email)->pluck('id');
        // $files = File::where('user_id', $user)->where('type', 'txt')->where('dl_check', false)->get();

        // $fileName = $request->fileName;
        // $file = File::where('user_id', $user)->where('name', $fileName);
        // $path = $file->pluck('path')->first();

        // return Storage::disk('s3')->download('files/bbb@naver.com/'.'1.txt', 'test.txt', $headers);

        shell_exec('zip /mnt/c/capstone/test.zip -j /mnt/c/capstone/test/*/');
        
        $filepath = '/mnt/c/capstone/test.zip';
        $filesize = filesize($filepath);
        $path_parts = pathinfo($filepath);
        $filename = $path_parts['basename'];
        $extension = $path_parts['extension'];
        
        header("Pragma: public");
        header("Expires: 0");
        header("Content-Type: application/octet-stream");
        header("Content-Disposition: attachment; filename=$filename");
        header("Content-Transfer-Encoding: binary");
        header("Content-Length: $filesize");
        
        ob_clean();
        flush();
        readfile($filepath);

        // return Storage::download('files/bbb@naver.com/091'.$i.'.txt', 'test.txt', $headers);
    }

    public function getMusicList($email){
        $user = User::where('email', $email)->pluck('id')->first();
        $list = File::where('user_id', $user)->where('dl_check', false)->select('path', 'name')->get();
        return $list;
    }
}
