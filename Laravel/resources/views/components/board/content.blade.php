<center>
<div class="content">
    <img src="{{asset('images/pic/bg_2.png')}}" id="bg" />
    <div class="middle">
        <p id="user_name">유저명</p>
        <img src="img/user.png" id="profile" />
        <div class="cont_text">
            <p>{{$board->content}}</p>
        </div>
        <div class="video">
            <div class="post_under">
                <img src="{{asset('images/pic/free.png')}}" id="thmb" />
                <div class="text_area_1">
                    <img src="{{asset('images/pic/view.png')}}" id="view" />
                    <p id="view_t">{{$board->hits}}</p>
                    <img src="{{asset('images/pic/like_1.png')}}" id="like" />
                    <p id="like_t">{{$board->total_heart}}</p>
                    <img src="{{asset('images/pic/download.png')}}" id="download" />
                </div>
                <div class="text_area_2">
                    <p id="title">{{$board->title}}</p>
                </div>
            </div>
        </div>
        <div class="package">
            <?php $num=1 ?>
            @foreach($board->files as $list)
            <div class="video_list">
                <button type="button" class="package_btn" onclick="videoChange({{$list->pivot->file_id}}, event)">
                    <p id="pac_num">{{$num++}}<p id="pac_title">{{$list->song}}</p>
                </button>
            </div>
            @endforeach
        </div>
        <div class="background">
        </div>
    </div>
</div>
</center>