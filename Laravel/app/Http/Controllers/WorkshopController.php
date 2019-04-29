<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Storage;
use Aws\Laravel\AwsFacade;
use App\File;

class WorkshopController extends Controller
{
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
        return view('components.workshop.player')->with('url', $url);
    }

    public function cutter(Request $request){
        $ts = $request->temporary_sound;
        $tss = explode('/', $ts);
        $audioName = $tss[count($tss)-1];
        shell_exec("ffmpeg -i ".$ts." -c copy -ss ".$request->start_sec." -t ".$request->end_sec." -y /var/www/capstone/RhythmTataki/Laravel/public/song/clip/".$request->clip_name.".mp3");
        Storage::disk('s3')->delete('temporarySound/'.$audioName);

        $s3 = AwsFacade::createClient('s3');
        $s3->putObject([
            'Bucket' => 'capstone.rhythmtataki.bucket',
            'Key' => 'workshop/drumSoundClip/'.$request->clip_name.'.ogg',
            'SourceFile' => 'song/clip/'.$request->clip_name.".ogg",
        ]);

        $size = Storage::disk('s3')->size('workshop/drumSoundClip/'.$request->clip_name.'.ogg');

        File::create([
           'user_id' => \Auth::user()->id,
           'path' => Storage::disk('s3')->url('workshop/drumSoundClip/'),
           'name' => $request->clip_name.".ogg",
           'type' => "ogg",
           'size' => round($size/1000, 1),
        ]);

        Shell_exec("rm /var/www/capstone/RhythmTataki/Laravel/public/song/clip/".$request->clip_name.".ogg");

        // return response()->json($request->clip_name, 200, [], JSON_PRETTY_PRINT);
        return redirect('/workshop');
    }
}
