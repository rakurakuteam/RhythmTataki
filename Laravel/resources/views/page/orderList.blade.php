@extends('master')

@section('title')
    주문
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/store/order.css')}}" />
@endsection

@section('header')
    @include('layouts.header.store')
@endsection

@section('nav')
    
@endsection

@section('body')
    @include('components.store.orderList')
@endsection

@section('footer')
@endsection

@push('scripts')
@endpush