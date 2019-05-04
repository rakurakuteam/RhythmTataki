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
    //Drag기능
    dropZone.on('dragenter', function (e) {
        e.stopPropagation();
        e.preventDefault();
        // 드롭다운 영역 css
        dropZone.css('background-color', '#E3F2FC');
    });
    dropZone.on('dragleave', function (e) {
        e.stopPropagation();
        e.preventDefault();
        // 드롭다운 영역 css
        dropZone.css('background-color', '#FFFFFF');
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
        dropZone.css('background-color', '#FFFFFF');
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

    if (fileObject != null) {
        // 파일 Drag 이용하여 등록시
        files = fileObject;
    } else {
        // 직접 파일 등록시 files = $('#multipaartFileList_' + fileIndex)[0].files;
        console.log('error');
    }
    var fileName = files[0].name;
    var fileNameArr = fileName.split("\.");
    // 확장자
    var ext = fileNameArr[fileNameArr.length - 1];
    FileSize = (files[0].size / 1024 / 1024).toFixed(2);
    console.log(ext);

    if($.inArray(ext, ['mp3', 'wav'])){
        alert("등록 불가 확장자");
        return;
    };

    uploadFile(files);
}

// 파일 등록
function uploadFile(fileObject) {
    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
    });

    // 용량을 50MB를 넘을 경우 업로드 불가
    if (FileSize > maxUploadSize) {
        // 파일 사이즈 초과 경고창
        alert("용량 초과\n업로드 가능 용량 : " + maxUploadSize + " MB");
        return;
    }

    var form = $('#uploadForm');
    var formData = new FormData(form);
    formData.append('audio', fileObject[0]);

    $.ajax({
        data: formData,
        type: "POST",
        url: "/workshop/upload",
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data);
            $("#workshop").html(data);
        },
        error: function (data) {
            console.log(data.status);
        }
    });

}
