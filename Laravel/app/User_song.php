<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class User_song extends Model
{
    protected $fillable = [
        'file_id', 'name'
    ];

    // 일대일
    public function score(){
        return $this->hasOne('App\Score');
    }

    public function file(){
        return $this->belongsTo('App\File');
    }
}
