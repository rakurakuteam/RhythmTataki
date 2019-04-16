<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Score extends Model
{
    public $timestamps = false;

    protected $fillable = [
        'user_id', 'song_id', 'score', 'created_at'
    ];

    // 일대다 역
    public function song(){
        return $this->belongsTo('App\Song');
    }
}
