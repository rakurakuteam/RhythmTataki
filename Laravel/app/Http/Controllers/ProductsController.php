<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Log;
use Illuminate\Http\Request;
use App\Cart;

class ProductsController extends Controller
{
    public function addCart(Request $request)
    {
        Log::info('addCartLog / user : '.\Auth::user()->id." / product : ".$request->id);
        if(\Auth::check() && !Cart::where('user_id', \Auth::user()->id)->where('product_id', $request->id)->exists()){
            Log::info('addCartTest 2nd / user : '. \Auth::user()->id);
            Cart::create([
                'product_id' => $request->id,
                'user_id' => \Auth::user()->id,
            ]);
        }elseif(!\Auth::check()){
            return "로그인해 주세요";
        }else{
            return "이미 등록된 상품";
        }
        return "등록완료";
    }
}
