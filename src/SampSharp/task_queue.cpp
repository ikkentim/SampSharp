#include "task_queue.h"
#include <assert.h>

task_queue::promise task_queue::enqueue(promise_function func) {
    std::packaged_task<void*()> task(func);

    std::future<void*> fut = task.get_future();

    /* use a small block to release the lock as soon as possible. */
    {
        std::lock_guard<std::mutex> lock(mutex_);
        tasks_.push_back(std::move(task));
    }

    return fut;
}

int task_queue::count() {
    std::lock_guard<std::mutex> lock(mutex_);
    return tasks_.size();
}

void task_queue::run_one(std::unique_lock<std::mutex>* lock) {
    assert(lock);

    std::packaged_task<void*()> task(std::move(tasks_.front()));
    tasks_.pop_front();

    /* unlock during task. */
    lock->unlock();
    task();
    lock->lock();
}

void task_queue::run_all() {
    std::unique_lock<std::mutex> lock(mutex_);

    while (!tasks_.empty()) {
        run_one(&lock);
    }
}

void task_queue::run_all_for(int ms) {
    clock_t start = clock();
    clock_t clock_lim = CLOCKS_PER_SEC / (1000/ms);

    do {
        run_all();
    } while (clock() - start < clock_lim);
}
