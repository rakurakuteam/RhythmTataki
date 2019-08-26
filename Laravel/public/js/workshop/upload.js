// 등록할 파일 사이즈
var FileSize = 0;
// 등록 가능한 파일 사이즈 MB
const maxUploadSize = 50;

$(function () {
    // 파일 드롭 다운
    fileDropDown();
});

// 파일 드롭 다운
function fileDropDown() {
    var dropZone = $("#dropZone");

    dropZone.on('dragleave', function (e) {
        e.stopPropagation();
        e.preventDefault();
        // 드롭다운 영역 css
        dropZone.css('background-color', 'rgba(236, 234, 234, 0.8)');
        dropZone.css('border-style', 'none');
    });
    dropZone.on('dragover', function (e) {
        e.stopPropagation();
        e.preventDefault();
        // 드롭다운 영역 css
        dropZone.css('background-color', '#E3F2FC');
        dropZone.css('border-style', 'solid');
        dropZone.css('border-color', 'green');
    });
    dropZone.on('drop', function (e) {
        e.preventDefault();
        // 드롭다운 영역 css
        dropZone.css('background-color', 'rgba(236, 234, 234, 0.8)');
        dropZone.css('border-style', 'none');

        var files = e.originalEvent.dataTransfer.files;

        if (files != null) {
            if (files[0].type == "") {
                alert("오디오 파일을 업로드해 주세요");
                return;
            }
            if (files.length > 1) {
                alert("하나의 파일만 업로드해 주세요");
                return;
            }
            selectFile(files)
        } else {
            alert("ERROR");
        }
    });
}

// 파일 선택시
function selectFile(fileObject) {
    var files = null;

    if (fileObject != null) {// 파일 Drag 이용하여 등록시
        files = fileObject;
    } else {// 직접 파일 등록시
        files = $('#ex_file')[0].files;
        console.log('select');
    }

    var fileName = files[0].name;
    var fileNameArr = fileName.split("\.");
    var ext = fileNameArr[fileNameArr.length - 1]; // 확장자
    FileSize = (files[0].size / 1024 / 1024).toFixed(2);
    console.log(ext);

    if($.inArray(ext, ['mp3', 'ogg']) < 0){
        alert('사용할 수 없는 확장자입니다.');
        return;
    };

    uploadFile(files);
}

// 파일 등록
function uploadFile(fileObject) {
    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        }
    });

    // 용량을 50MB를 넘을 경우 업로드 불가
    if (FileSize > maxUploadSize) {
        // 파일 사이즈 초과 경고창
        alert("용량 초과\n업로드 가능 용량 : " + maxUploadSize + " MB");
        return;
    }

    // var form = $('#uploadForm');
    var formData = new FormData();
    formData.append('audio', fileObject[0]);
    
    var back = $('<div id="back" style="height:100%; width:100%; background-color:rgba(0,0,0,0.6); top:0; left:0; position:fixed; z-index:100"></div>');
    var loading = $('<img src="images/pic/loading.gif" id="loading"/>'); // 로딩 이미지

    $('body').append(back);
    $('#back').append(loading);

    $.ajax({
        data: formData,
        type: "POST",
        url: "/workshop/upload",
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            console.log('success');
            $("#workshop_center").unwrap() //부모 요소를 삭제
            $(".filebox").remove(); //업로드 박스 삭제
            $(".contents_box").append(data); //웨이브 폼 삽입
        },
        error: function (data) {
            console.log(data.status);
        }
    });
}