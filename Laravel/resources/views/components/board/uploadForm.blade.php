<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Document</title>
</head>
<body>
    <form action="{{route('unity.fileUpload')}}" method="POST" enctype="multipart/form-data">
        @csrf
        <input type="file" name="file[]" multiple>
        <input type="text" name="email" id="email">
        <input type="submit" name="submit" value="전송">
    </form>
</body>
<!-- 유니티 파일 업로드 테스트 페이지 -->
</html>
