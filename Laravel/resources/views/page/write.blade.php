@extends('master')

@section('title')
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/writeForm/write.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/footer.css')}}" />
    <link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" id="bootstrap-css">
@endsection

@section('header')
    @include('layouts.header.store')
@endsection

@section('body')
    <div class="comment_box">
      @include('components.writeForm.writeForm')
      @include('components.writeForm.list')
    </div>
@endsection

@section('footer')
@include('layouts.footer.footer')
@endsection

@push('scripts')
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" href="{{asset('js/writeForm/write.js')}}"></script>
@endpush
