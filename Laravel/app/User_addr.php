<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class User_addr extends Model
{
    public $timestamps = false;

    // 일대다
    public function boards(){
        return $this->hasMany('App\Order');
    }

    // 일대다 역
    public function address(){
        return $this->belongsTo('App\Address', 'addr_id');
    }

    // 다대다
    public function products(){
        return $this->belongsToMany('App\Product', 'orders');
    }
}
