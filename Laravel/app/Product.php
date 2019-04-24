<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Product extends Model
{
    // 다대다
    public function users(){
        return $this->belongsToMany('App\User', 'carts');
    }

    // 일대다
    public function images(){
        return $this->hasMany('App\Image');
    }

    // 다대다
    public function user_addrs(){
        return $this->belongsToMany('App\User_addr', 'orders');
    }
}
