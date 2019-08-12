<contents>
    <div class="under_box">
        <div class="title_area">
            <h2 id="title">{{__('messages.product_list')}}</h2>
        </div>

        @foreach($products as $product)
            <div class="contents_box">
                <center>
                    <!--이미지-->
                    <div class="image">
                        <a href="{{route('store.show', $product->id)}}">
                            <img src="{{$product->images[0]->path.$product->images[0]->name}}" id="shop_img" />
                        </a>
                    </div>
                    <!--텍스트-->
                    <div class="text">
                        <a href="{{route('store.show', $product->id)}}">
                            <h2>{{$product->name}}</h2>
                        </a>
                        <p id="price_text">{{number_format($product->price)}}원</p>
                    </div>
                    <div class="button">
                    <!--주문버튼-->
                    <button class="shop_button order_button" onclick="location.href='{{route('store.show', ['id' => $product->id])}}'">
                            <b>{{__('messages.orderRequest')}}</b>
                    </button>
                    </div>
                </center>
            </div> 
        @endforeach
    </div>
</contents>