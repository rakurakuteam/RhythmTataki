@extends('master')

@section('title')
    주문확인
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/store/orderConfirm.css')}}" />
@endsection

@section('header')
    @include('layouts.header.store')
@endsection

@section('nav')
    
@endsection

@section('body')
    @include('components.store.payPage')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
@endpush