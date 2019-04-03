<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Board extends Model
{

    protected $fillable = [
        'user_id', 'title', 'content', 'hits', 'hearts'
    ];

    public function hearts(){
        return $this->HasMany('App\Heart');
    }

    public function user(){
        return $this->belongsTo('App\User');
    }
}
