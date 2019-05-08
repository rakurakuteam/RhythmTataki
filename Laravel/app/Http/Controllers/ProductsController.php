<?php

namespace App\Http\Controllers;

use Illuminate\Support\Facades\Log;
use Illuminate\Support\Facades\DB;
use Illuminate\Http\Request;
use App\User;
use App\Cart;
use App\Product;
use App\Address;
use App\User_addr;
use App\Order;

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

    // 장바구니 지우기
    public function removeCart(Request $request){
        Cart::where('user_id', \Auth::user()->id)->where('product_id', $request->product_id)->delete();
        $cart = Cart::where('user_id', \Auth::user()->id)->pluck('product_id');
        $products = Product::with(['images' => function($query){
            $query->where('type', 'thumbnail')->select('product_id', 'path', 'name');
        }])->whereIn('id', $cart)->select('id', 'name', 'price')->get();
        
        // return $products;
        return view('components.store.basketBox')
        ->with('carts', $products)
        ->with('count', 0);;
    }

    // 주문 페이지
    public function orderSheet($id)
    {
        if(strpos($id, ',')){
            $product_id = explode(',', substr($id, 1, -1));
        }else{
            $product_id = (array)$id;
        }
        
        $products = Product::with(['images' => function($query){
            $query->where('type', 'thumbnail')->select('product_id', 'path', 'name');
        }])->whereIn('id', $product_id)->get();

        $user = User::find(\Auth::user()->id);
        $address = $user->addresses()
        ->where('user_id', \Auth::user()->id)->where('rep', true)->get();

        $user = User::where('id', \Auth::user()->id)->select('id', 'name', 'phone')->first();
        
        $price = $this->price($products);
        $delivery = 2500;

        if($price > 30000){
            $delivery = 0;
        }
        
        return view('page.orderSheet')
        ->with('products', $products)
        ->with('address', $address)
        ->with('count', 0)
        ->with('delivery', $delivery)
        ->with('price', $price)
        ->with('user', $user);
    }

    // 총액
    public function price($query){
        $price = $query->map(function ($product){
            return $product->price;
        })->sum();
        return $price;
    }

    // 주문처리
    public function order(Request $request){
        // $user = User::find(\Auth::user()->id);

        // $address = Address::create([
        //     'zip_code' => $request->zip_code,
        //     'addr_1' => $request->address1,
        //     'addr_2' => $request->address2,
        //     'created_at' => now(),
        // ]);

        // if(isset($request->cb_2)){
        //     $user_addr = $user->addresses()->attach($address->id, ['rep' => true]);
        // }else{
        //     $user_addr = $user->addresses()->attach($address->id);
        // }
        
        // if(Order::)
        // $num = str_replace('-', '', now()->toDateString());
        // $last = substr($num, -1);
        // $num = sprintf("%03d", ++$last);

        // $order = Order::create([
        //     'order_num' => str_replace('-', '', now()->toDateString()).sprintf("%04d",$i),
        //     'product_id' => $request->product_id,
        //     'quantity' => $request->quantity,
        //     'user_addr_id' => $user_addr_id,
        //     'request' => $request->order_request,
        //     'status_id' => 0,
        //     'payment' => $request->pos,
        //     'created_at' => now(),
        // ]);

        return $request->all();
        return $num;
    }

    // 결제 페이지
    public function payPage(Request $request){
        $orders = Order::with('product', 'delivery_status')
        ->whereIn('id', $request)->where('status_id', 0)->first();

        return view('page.payPage');
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
        $product_id = Cart::where('user_id', \Auth::user()->id)->pluck('product_id');
        $products = Product::with(['images' => function($query){
            $query->where('type', 'thumbnail')->select('product_id', 'path', 'name');
        }])->whereIn('id', $product_id)->select('id', 'name', 'price')->get();
        
        $price = $this->price($products);

        return view('page.carts')
        ->with('carts', $products)
        ->with('count', 0)
        ->with('product_id', $product_id)
        ->with('total_price', $price);
    }

    // 주문 확인 페이지
    public function orderList(){
        $addr = User_addr::where('user_id', \Auth::user()->id)->pluck('id');

        $orders = Order::with(['user_addr' => function($query){
            $query->where('user_id', \Auth::user()->id);
        }, 'delivery_status', 
        'product' => function($query){
            $query->with(['images' => function($query){
                $query->where('type', 'thumbnail')->select('product_id', 'path', 'name');
            }])->select('id', 'name', 'price')->get();
        }])->whereIn('user_addr_id', $addr)->get();
        
        return view('page.orderList'); 
        // return $orders; 
    }
}
