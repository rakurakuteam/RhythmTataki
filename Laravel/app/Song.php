<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Song extends Model
{
    public $timestamps = false;

    // 일대다 역
    public function user_songs(){
        return $this->hasMany('App\User_song');
    }
}
