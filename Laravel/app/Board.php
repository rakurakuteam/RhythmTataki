<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Board extends Model
{

    protected $fillable = [
        'user_id', 'title', 'content', 'hits', 'hearts', 'dl_check'
    ];

    // 일대다
    public function hearts(){
        return $this->HasMany('App\Heart');
    }

    // 일대다 역
    public function user(){
        return $this->belongsTo('App\User');
    }

    // 다대다
    public function files(){
        return $this->belongsToMany('App\File');
    }

    // 다대다
    public function users(){
        return $this->belongsToMany('App\User', 'order');
    }
}
