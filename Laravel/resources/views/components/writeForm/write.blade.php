<contents>
        <div class="contents">
            <!--상단이미지-->
            <div class="img_box">
                <p id="title_text"><b><i>WRITE</i></b></p>
                <img src="{{asset('images/pic/drum_img.png')}}" id="drum_img" />
            </div>
            <!--글쓰기폼-->
            <center>
                <div class="form_group">
                    <div class="form_box">
                        <div class="name_box">
                            <p id="name"><b>작성자</b></p><input type="text" class="name_input" placeholder="작성자명" />
                        </div>
                        <div class="title_box">
                            <p id="title"><b>제목</b></p><input type="text" class="title_input" placeholder="제목을 입력 해 주세요" />
                        </div>
                        <div class="comment_box">
                            <label for="comment" id="comment"><b>내용</b></label>
                            <textarea class="comment_input" placeholder="글 내용을 입력 해 주세요"></textarea>

                        <!--리스트-->
                            @include('components.writeForm.list')
                            <button type="submit" class="submit_btn"><b>작성완료</b></button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </contents>