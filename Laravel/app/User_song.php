<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class User_song extends Model
{
    public $timestamps = false;

    protected $fillable = [
        'user_id', 'song_num', 'song_id', 'file_id'
    ];

    // 일대일
    public function score(){
        return $this->hasOne('App\Score');
    }
    
    // 일대다 역
    public function song(){
        return $this->belongsTo('App\Song');
    }

    // 일대다 역
    public function file(){
        return $this->belongsTo('App\File');
    }
    
    // 일대다 역
    public function user(){
        return $this->belongsTo('App\User');
    }

}
