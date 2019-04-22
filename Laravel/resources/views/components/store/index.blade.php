<contents>
    <div class="under_box">
        <div class="title_area">
            <h2 id="title">상품목록</h2>
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
                        <!--주문버튼-->
                        <div class="button">
                            <button id="shop_button" onclick="location.href='{{route('orderListPage')}}'">
                                <b>{{__('messages.orderRequest')}}</b>
                            </button>
                        </div>
                </center>
            </div> 
        @endforeach
    </div>
</contents>