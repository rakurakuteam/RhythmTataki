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
    @include('layouts.header.main')
@endsection

@section('body')
    @include('components.writeForm.write')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script>
    {{-- <script src="//code.jquery.com/jquery-1.11.1.min.js"></script> --}}
    <script type="text/javascript" href="{{asset('js/writeForm/write.js')}}"></script>
@endpush
