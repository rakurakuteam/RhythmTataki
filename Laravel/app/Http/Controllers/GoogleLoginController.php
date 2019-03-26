<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Socialite;

class GoogleLoginController extends Controller
{
        
    public function googleLogin()
    {
        return Socialite::driver('Google')->redirect();
    }

    public function googleCallback()
    {
        $user = Socialite::driver('Google')->user();

        return redirect('/');
        // $user->token;
    }
}
