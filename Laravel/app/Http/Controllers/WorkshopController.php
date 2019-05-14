<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Storage;
use Aws\Laravel\AwsFacade;
use App\File;
use Illuminate\Support\Facades\Log;
use Alert;

class WorkshopController extends Controller
{
    public function __construct()
    {
        $this->middleware('auth');
    }
    
    public function index(Request $request){
        return view('page.workshop');
    }

    public function upload(Request $request){
        $audioName = time().'.'.$request->audio->getClientOriginalExtension();

        $path = $request->file('audio')->storeAs(
            'workshop/temporarySound',$audioName, 's3'
        );

        $url = Storage::disk('s3')->url($path);

        // return response()->json($request->audio, 200, [], JSON_PRETTY_PRINT);
        return view('components.workshop.player')
        ->with('url', $url);
    }

    public function cutter(Request $request){
        $ts = $request->temporary_sound;
        $tss = explode('/', $ts);
        $audioName = $tss[count($tss)-1];

        shell_exec("ffmpeg -i ".$ts." -f wav -ss ".$request->start_sec." -t ".$request->len." -y /mnt/c/capstone/RhythmTataki/Laravel/public/song/clip/".$request->clip_name.".wma");

        Storage::disk('s3')->delete('workshop/temporarySound/'.$audioName);

        $s3 = AwsFacade::createClient('s3');
        $s3->putObject([
            'Bucket' => 'capstone.rhythmtataki.bucket',
            'Key' => 'workshop/drumSoundClip/'.\Auth::user()->email.'/'.$request->clip_name.'.wma',
            'SourceFile' => 'song/clip/'.$request->clip_name.".wma",
        ]);

        $size = Storage::disk('s3')
        ->size('workshop/drumSoundClip/'.\Auth::user()->email.'/'.$request->clip_name.'.wma');

        File::create([
           'user_id' => \Auth::user()->id,
           'path' => Storage::disk('s3')->url('workshop/drumSoundClip/'.\Auth::user()->email.'/'),
           'name' => $request->clip_name.".wma",
           'type' => "wma",
           'size' => round($size/1000, 1),
        ]);

        Shell_exec("rm /var/www/capstone/RhythmTataki/Laravel/public/song/clip/".$request->clip_name.".wma");

        // return response()->json($request->clip_name, 200, [], JSON_PRETTY_PRINT);
        Alert::success('자르기 성공', '게임에서 확인해주세요.');
        return redirect('/workshop');
    }
}
