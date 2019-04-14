{{-- <contents>
    <center>
        <div class="content_box">
            @foreach($products as $product)
                <div class="sub_photo" id="photo_gallery">
                    <p class="thmb">
                        <a href="{{route('store.show', $product->id)}}">
                        <img src="{{$product->images[0]->path.$product->images[0]->name}}" class="picture" href="#"></a>
                    </p>
                    <a href="{{route('store.show', $product->id)}}">
                        <div>
                            <h3>
                                {{$product->name}}
                            </h3>
                        </div>
                    </a>
                    <div>{{number_format($product->price)}}원</div>
                    <button type="button">주문하기</button>
                </div>
            @endforeach
        </div>
    </center>
</contents> --}}

<contents>
    <div class="under_box">
        <div class="title_area">
            <h2 id="title">상품목록</h2>
        </div>

        <div class="contents_box">
            <center>
                @foreach($products as $product)
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
                        <button id="shop_button">
                            <b>{{__('messages.orderRequest')}}</b>
                        </button>
                    </div>
                @endforeach
            </center>
        </div> 
    </div>
</contents>