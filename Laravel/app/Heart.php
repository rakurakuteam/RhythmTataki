<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Heart extends Model
{
    protected $fillable = [
        'user_id', 'board_id', 'hits', 'heart'
    ];

    public function board(){
        return $this->belongsTo('App\Board');
    }
}
