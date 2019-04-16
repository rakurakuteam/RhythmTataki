<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class File extends Model
{
    // 다대다
    public function boards(){
        $this->belongsToMany('App\Board');
    }
    
    // 일대다 역
    public function user(){
        $this->belongsTo('App\User');
    }
}
