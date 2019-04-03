<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class User_addr extends Model
{
    public $timestamps = false;

    public function boards(){
        return $this->hasMany('App\Board');
    }
}
