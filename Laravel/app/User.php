<?php

namespace App;

use Illuminate\Notifications\Notifiable;
use Illuminate\Contracts\Auth\MustVerifyEmail;
use Illuminate\Foundation\Auth\User as Authenticatable;

class User extends Authenticatable
{
    use Notifiable;

    protected $fillable = [
        'name', 'email', 'password', 'confirm' , 'token', 'token_exp',
    ];

    protected $hidden = [
        'password', 'token',
    ];

    // 리멤버 토큰x
    public function getRememberTokenName(){
        return null; 
    }

    public function address(){
        return $this->belongsToMany('App\Address', 'user_addr', 'user_id', 'addr_id');
    }

    public function products(){
        return $this->belongsToMany('App\Product', 'carts');
    }
}
