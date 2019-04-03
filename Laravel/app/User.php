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

    public function address(){
        return $this->belongsToMany('App\Address', 'user_addr', 'user_id', 'addr_id');
    }

    public function getRememberTokenName(){
        return null; // not supported
    }
}
