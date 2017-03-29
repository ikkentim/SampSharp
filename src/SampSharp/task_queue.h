#pragma once

#include <deque>
#include <mutex>
#include <future>

class task_queue
{
public:
    typedef std::future<void*> promise;
    typedef std::function<void*()> promise_function;// void(*promise_function)(void);

    promise enqueue(promise_function);
    int count();
    void run_one(std::unique_lock<std::mutex>* lock);
    void run_all();
    void run_all_for(int ms);
private:
    std::deque<std::packaged_task<void*()>> tasks_;
    std::mutex mutex_;
};

