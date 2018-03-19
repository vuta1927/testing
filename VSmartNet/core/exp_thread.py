import os, time
import threading, queue

class WorkerThread(threading.Thread):
    def __init__(self, dir_q, result_q):
        super(WorkerThread, self).__init__()
        self.dir_q = dir_q
        self.result_q = result_q
        self.stop_request = threading.Event()

    def run(self):
        while not self.stop_request.isSet():
            try:
                dirname = self.dir_q.get(True, 0.05)
                filenames = list(self._files_in_dir(dirname))
                self.result_q.put((self.name, dir, filenames))
            except queue.Empty:
                continue
    
    def join(self, timeout=None):
        self.stop_request.set()
        super(WorkerThread, self).join(timeout)

    def _files_in_dir(self, dirname):
        for path, dirs, files in os.walk(dirname):
            for file in files:
                yield os.path.join(path, file)

def main(args):
    dir_q = queue.Queue()
    result_q = queue.Queue()

    pool = [WorkerThread(dir_q=dir_q, result_q=result_q) for i in range(4)]

    for thread in pool:
        thread.start()

    work_count = 0
    for dir in args:
        if os.path.exists(dir):
            work_count += 1
            dir_q.put(dir)
    print('Assigned %s dirs to workers' % work_count)

    while work_count > 0:
        result = result_q.get()
        print('From thread %s: %s files found in dir %s' %(result[0], len(result[2]), result[1]))
        work_count -= 1

    for thread in pool:
        thread.join()

if __name__ == '__main__':
    import sys
    main(sys.argv[1:])