<contents>
    <center>
        <div class="posts" id="posts">
            <div class="title_area" id="title_area">
                <a href="userPage.php">
                    <img src="{{asset('images/pic/profile.png')}}" id="profile"/>
                </a>
                <h1 id="title">{{$board->title}}</h1>
            </div>
            <div class="sub_photo" id="photo_gallery">
                <p class="thmb">
                    <a href="#">
                        <div id="video_box">
                            <img src="{{asset('images/pic/free.png')}}"></div>
                            <div id="playButton_box">
                                <img src="{{asset('images/pic/playButton.png')}}" id="play_button"/>
                            </div>
                        </div>
                    </a>
                </p>
                <div class="view_like">
                    <img src="{{asset('images/pic/view.png')}}" class="view"/>
                    <p class="view_text">{{$board->hits}}</p>
                    <img src="{{asset('images/pic/like_1.png')}}" class="like"/>
                    <p class="like_text">{{$heart}}</p>
                    <img src="{{asset('images/pic/download.png')}}" class="download"/>
                </div>
                <div class="write_box">
                    <p>{{$board->content}}</p>
                </div>
            </div>
        </div>
    </div>
</center>
</contents>