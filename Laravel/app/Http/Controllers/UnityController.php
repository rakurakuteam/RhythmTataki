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
use App\User_song;

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
            User_song::create([
                'user_id' => $user->id,
                'song_num' => $i,
                'song_id' => $i,
                'file_id' => null,
            ]);
            Score::create([
                'user_id' => $user->id,
                'user_song_id' => $i,
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
        
        $user = User::where('email', $request->email);
        $user_song = User_song::where('user_id', $user->value('id'));

        if($user->exists() && Hash::check($request->pw, $user->value('password'))){
            Log::info('로그인 성공');
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
        $song = User_song::where('song_num', $request->song)->value('id');
        $score = Score::where('user_id', $user)->where('user_song_id', $song);

        if($score->where('score', '<', $request->score)->exists()){
            $score->update(['score' => $request->score,
                            'created_at' => now()]);
            return 1;
        }
        return 0;
    }

    //단일점수
    public function getScore($email, $song){
        Log::info('email = '.$email.' / song = '.$song);
        $user = User::where('email', $email)->value('id');
        $score = Score::join('user_songs', 'scores.user_song_id', '=', 'user_songs.id')
        ->select('user_song_id', 'song_num', 'score')
        ->where('scores.user_id', $user)->where('song_num', $song)->first();
        Log::info('score = '.$score);
        // key     :  value
        // 노래id :  점수
        
        $k_v_score[$score->song_num] = $score->score; 
        
        // return $score;
        return json_encode($k_v_score);
    }

    //점수들
    public function getScores($email){
        Log::info('email = '. $email);
        $user = User::where('email', $email)->value('id');
        $scores = Score::join('user_songs', 'scores.user_song_id', '=', 'user_songs.id')
        ->where('scores.user_id', $user)->get();
        Log:info('scores = '.$scores);
        // key     :  value
        // 노래id :  점수
        $k_v_scores = [];
        foreach($scores as $score){
            $k_v_scores[$score->song_num] = $score->score;
        }

        // for($i=1; $i<=count($scores); $i++){
        //     $k_v_scores[$i] = $score->score;
        // }
        
        // return $scores;
        return json_encode($k_v_scores);
    }

    // 파일업로드
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

            $url = Storage::disk('s3')->url('files/'.$request->email.'/');
            $size = round($request->file[$i]->getClientSize()/1024/1024, 2);

            Log::info('file path:'. $path);
            Log::info('file url:'. $url);
            Log::info('file name:'. $request->file[$i]->getClientOriginalName());
            Log::info('file type:'. $request->file[$i]->getClientOriginalExtension());
            Log::info('file size:'. $request->file[$i]->getClientSize());
            
            $file = File::create([
                'user_id' => $user,
                'path' => $url,
                'name' => $fileName,
                'type' => $fileType,
                'size' => $size,
                'dl_check' => true,
                'created_at' => now(),
            ]);
        }   

        $user_song_num = User_song::where('user_id', $user)->max('song_num')+1;
        
        $user_song = User_song::create([
            'user_id' => $user,
            'song_num' => $user_song_num,
            'song_id' => null,
            'file_id' => $file->id
        ]);

        Score::create([// 초기 점수 생성
            'user_id' => $user,
            'user_song_id' => $user_song->id,
            'score' => 0,
        ]);
        return "업로드 성공";
    }

    // 파일 다운로드
    public function fileDownload(Request $request)
    {
        Log::info('email: '.$request->email);

        shell_exec('zip /mnt/zip-point/'.$request->email.'/songs.zip -j /mnt/zip-point/'.$request->email.'/*');

        $filepath = '/mnt/zip-point/'.$request->email.'/songs.zip';
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
      
	    shell_exec('rm -r /mnt/zip-point/'.$request->email);

	    $u_id = User::where('email', $request->email)->value('id');
        File::where('user_id', $u_id)->whereIn('type', ['txt', 'ogg'])->update(['dl_check' => true]);
    }

    public function drumSoundDownload($email)
    {
        $user = User::where('email', $email)->value('id');
        $lists = File::where('user_id', $user)->where('dl_check', false)->select('id', 'path', 'name')->get();
        shell_exec('mkdir /mnt/zip-point/drumSound/'.$email);
        foreach($lists as $list){
            shell_exec('cp /mnt/mountpoint/workshop/drumSoundClip/'.$email.'/'.$list->name. ' /mnt/zip-point/drumSound/'.$email.'/'.$list->name);
        }
        shell_exec('zip /mnt/zip-point/drumSound/'.$email.'/drumSound.zip -j /mnt/zip-point/drumSound/'.$email.'/*');

        $filepath = '/mnt/zip-point/drumSound/'.$email.'/drumSound.zip';
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

        shell_exec('rm -r /mnt/zip-point/drumSound/'.$email);

	    $u_id = User::where('email', $email)->value('id');
        File::where('user_id', $u_id)->where('type', 'wma')->update(['dl_check' => true]);
        // return json_encode($list);
    }
}
