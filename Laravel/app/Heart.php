<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Heart extends Model
{
    protected $fillable = [
        'user_id', 'board_id', 'hits', 'heart', 'dl_check'
    ];

    // 일대다 역
    public function board(){
        return $this->belongsTo('App\Board');
    }

    // 일대다 역
    public function user(){
        return $this->belongsTo('App\User');
    }
}
