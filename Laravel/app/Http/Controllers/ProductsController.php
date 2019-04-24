<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Log;
use Illuminate\Support\Facades\DB;
use Illuminate\Http\Request;
use App\Cart;
use App\Product;
use App\Address;
use App\Order;
use App\User_addr;

class ProductsController extends Controller
{
    // 로그인 한 사용자만 이용가능
    public function __construct()
    {
        $this->middleware('auth');
    }

    // 장바구니 담기
    public function addCart(Request $request)
    {
        if(\Auth::check()){
            Log::info('addCartLog / user : '.\Auth::user()->id." / product : ".$request->id);
            if(\Auth::check() && !Cart::where('user_id', \Auth::user()->id)->where('product_id', $request->id)->exists()){
                Log::info('addCartTest 2nd / user : '. \Auth::user()->id);
                Cart::create([
                    'product_id' => $request->id,
                    'user_id' => \Auth::user()->id,
                ]);
            }else{
                return "이미 등록된 상품";
            } 
            return "등록완료";
        }
        return "로그인해 주세요";
    }

    // 주문 페이지
    public function orderList(Request $request)
    {
        $products = Product::with(['images' => function($query){
            $query->where('type', 'thumbnail')->select('product_id', 'path', 'name');
        }])->where('id', $request->id)->get();

        $address = User_addr::with('address')
        ->where('user_id', \Auth::user()->id)->where('rep', true)->get();

        // return $products;
        return view('page.order')
        ->with('products', $products)
        ->with('address', $address)
        ->with('count', 0);
    }

    // 주문처리
    public function order(Request $request){
        $order = Order::create([
            'order_num' => str_replace('-', '', now()->toDateString()).sprintf("%03d",$i),
            'product_id' => $request->product_id,
            'quantity' => $request->quantity,
            'user_addr_id' => $request->address,
            'request' => $request->request,
            'status_id' => 0,
            'payment' => $request->payment,
            'created_at' => now(),
        ]);
        return response()->json($order, 200, [], JSON_PRETTY_PRINT);
    }

    // 결제 페이지
    public function payPage(Request $request){
        $orders = Order::with('product', 'delivery_status')
        ->whereIn('id', $request)->where('status_id', 0)->first();


        return response()->json($orders, 200, [], JSON_PRETTY_PRINT);
    }

    // 결제 처리
    public function pay(Request $request){
        $order = Order::whereIn('order_num', $request->order_num)->update(['status_id', 1]);

        return response()->json($orders, 200, [], JSON_PRETTY_PRINT);
    }

    // 장바구니 페이지
    public function cartPage()
    {
        $carts = Cart::where('user_id', \Auth::user()->id)->pluck('product_id');
        $products = Product::with(['images' => function($query){
            $query->where('type', 'thumbnail')->select('product_id', 'path', 'name');
        }])->whereIn('id', $carts)->select('id', 'name', 'price')->get();
        
        // return $products;
        return view('page.carts')
        ->with('carts', $products)
        ->with('count', 0);
    }

    // 주문 확인 페이지
    public function orderSheet(){
        $addr = User_addr::where('user_id', \Auth::user()->id)->pluck('id');

        $orders = Order::with(['user_addr' => function($query){
            $query->where('user_id', \Auth::user()->id);
        }, 'delivery_status', 
        'product' => function($query){
            $query->with(['images' => function($query){
                $query->where('type', 'thumbnail')->select('product_id', 'path', 'name');
            }])->select('id', 'name', 'price')->get();
        }])->whereIn('user_addr_id', $addr)->get();
        
        return response()->json($orders, 200, [], JSON_PRETTY_PRINT);
    }

    public function quantity(Request $request){
        Log::info('$request : '. $request->quantity);
        return 1;
    }
}
