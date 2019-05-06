@extends('master')

@section('title')
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/writeForm/write.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/footer.css')}}" />
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
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
<<<<<<< HEAD
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    <script type="text/javascript" href="{{asset('js/writeForm/write.js')}}"></script>
=======
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
>>>>>>> 707ad2b75fcd5f10578f7a05e033ece810d1ee03
@endpush
