<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Image extends Model
{
    public $timestamps = false;

    // 일대다 역
    public function product(){
        return $this->belongsTo('App\Product');
    }
}
