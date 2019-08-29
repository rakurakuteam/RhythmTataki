@extends('master')

@section('title')
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    {{-- <link rel="stylesheet" type="text/css" href="{{asset('css/writeForm/write.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/footer.css')}}" />
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css"> --}}
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link rel="stylesheet" type="text/css" href="{{asset('css/writeForm/write.css')}}" />
@endsection

@section('header')
    @include('layouts.header.main')
@endsection

@section('body')
    @include('components.writeForm.write')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script> {{-- 부트스트랩 --}}
@endpush
