<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Address extends Model
{
    // 다대다
    public function users(){
        return $this->belongsToMany('App\User', 'user_addr');
    }

    // 일대다
    public function user_addrs(){
        return $this->hasMany('App\User_addr', 'addr_id');
    }
}
