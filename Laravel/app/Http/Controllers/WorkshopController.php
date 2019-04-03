<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Storage;

class WorkshopController extends Controller
{
    public function index(Request $request){
        return view('page.workshop');
    }

    public function upload(Request $request){
        $audioName = time().'.'.$request->audio->getClientOriginalExtension();
        
        $path = $request->file('audio')->storeAs(
            'temporarySound',$audioName, 's3'
        );

        $url = Storage::disk('s3')->url($path);
        
        // return response()->json($request->audio, 200, [], JSON_PRETTY_PRINT);
        return view('components.workshop.player')->with('url', $url);
    }

    public function cutter(Request $request){
        $ts = $request->temporary_sound;
        $tss = explode('/', $ts);
        $audioName = $tss[count($tss)-1];
        shell_exec("ffmpeg -i ".$ts." -c copy -ss ".$request->start_sec." -t ".$request->end_sec." -y /mnt/c/capstone/RhythmTataki/Laravel/public/song/clip/".$request->clip_name.".mp3"); 
        Storage::disk('s3')->delete('temporarySound/'.$audioName);
        // shell_exec("aws s3 mv /mnt/c/capstone/RhythmTataki/Laravel/public/song/clip/tgun.mp3 s3://capstone.rhythmtataki.bucket/drumSoundClip/");
        // shell_exec("aws s3 mv /mnt/c/capstone/RhythmTataki/Laravel/public/song/clip/tgun.mp3 s3://capstone.rhythmtataki.bucket/drumSoundClip/");
        // shell_exec("mkdir /mnt/c/capstone/RhythmTataki/Laravel/public/song/clip/test");
        shell_exec("testshell ".$request->clip_name);
        return response()->json($request->clip_name, 200, [], JSON_PRETTY_PRINT);
    }
}