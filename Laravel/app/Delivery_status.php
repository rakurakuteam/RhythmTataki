<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Delivery_status extends Model
{
    public $timestamps = false;

    // 일대다
    public function orders(){
        return $this->hasMany('App\Order');
    }
}
