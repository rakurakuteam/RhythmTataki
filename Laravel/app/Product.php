<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Product extends Model
{
    public function users(){
        return $this->belongsToMany('App\User', 'carts');
    }

    public function images(){
        return $this->hasMany('App\Image');
    }
}
