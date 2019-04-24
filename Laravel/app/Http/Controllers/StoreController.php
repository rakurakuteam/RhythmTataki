<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Image;
use App\Product;

class StoreController extends Controller
{
    // 상품 목록
    public function index()
    {
        // $products = Product::all();
        
        $products = Product::all()->load(['images' => function($query){
            $query->where('type', 'thumbnail');
        }]);
        
        // $path = $products->images[0]->path;
        // $image = $products->images[0]->name;

        // return response()->json($products, 200, [], JSON_PRETTY_PRINT);
        return view('page.store')->with('products', $products);
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        //
    }
    
    // 상품 상세보기
    public function show($id)
    {
        $product = Product::find($id)->load(['images' => function($query){
            $query->where('type', 'original');
        }]);
        
        $path = $product->images[0]->path;
        $image = $product->images[0]->name;

        // return response()->json($product, 200, [], JSON_PRETTY_PRINT);
        return view('page.product')
        ->with('product', $product)
        ->with('path', $path)
        ->with('image', $image);
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, $id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy($id)
    {
        //
    }
}
