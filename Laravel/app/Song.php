<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Song extends Model
{
    public $timestamps = false;

    // 일대인 역
    public function user_song(){
        return $this->belongsTo('App\User_song');
    }
}
