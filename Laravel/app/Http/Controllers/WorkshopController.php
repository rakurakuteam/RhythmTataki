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
        return response()->json($request, 200, [], JSON_PRETTY_PRINT);
    }
}
