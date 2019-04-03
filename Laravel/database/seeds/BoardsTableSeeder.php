<?php

use Illuminate\Database\Seeder;

class BoardsTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        for($i=1; $i<=50; $i++){
            DB::table('boards')->insert([
                'user_id' => random_int(1, DB::table('users')->count()),
                'title' => "샘플 제목 ".$i,
                'content' => "샘플내용 : ".$i,
                'hits' => random_int(0, 500),
                'created_at' => now(),
            ]);
        }
    }
}
