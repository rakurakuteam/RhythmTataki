<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Order extends Model
{
    protected $fillable = [
        'order_num', 'product_id', 'quantity', 'user_addr_id', 'request', 'status_id', 'payment', 'created_at'
    ];

    // 일대다 역
    public function delivery_status(){
        return $this->belongsTo('App\Delivery_status', 'status_id');
    }

    // 일대다 역
    public function user_addr(){
        return $this->belongsTo('App\User_addr');
    }

    // 일대다 역
    public function product(){
        return $this->belongsTo('App\Product');
    }
}
