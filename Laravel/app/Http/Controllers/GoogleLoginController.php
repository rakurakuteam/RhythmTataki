<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Hash;
use Socialite;
use App\User;
use Carbon\Carbon;

class GoogleLoginController extends Controller
{
        
    public function googleLogin()
    {
        return Socialite::driver('Google')->redirect();
    }

    public function googleCallback()
    {
        $gooUser = Socialite::driver('Google')->user();
        
        /* 유저 테이블에 회원 정보가 없으면 등록*/
        if(!User::where('email', $gooUser->getEmail())->exists()){
            User::create([
                'email' => $gooUser->getEmail(),
                'password' => Hash::make($gooUser->token),
                'name' => $gooUser->getName(),
                'confirm' => true,
                'token' => $gooUser->token,
                'token_exp' => Carbon::now()->addHours(1),
            ]);
        }else{ /* 있으면 토큰값 업데이트 */
            User::where('email', $gooUser->getEmail())
                ->update(['token' => $gooUser->token,
                          'token_exp' => Carbon::now()->addHours(1)]);
        }

        if(Auth::loginUsingId(User::where('email', $gooUser->getEmail())->value('id'), true))
        {
            return redirect('/workshop');
        }

        return response()->json($gooUser, 200, [], JSON_PRETTY_PRINT);
    }
}
