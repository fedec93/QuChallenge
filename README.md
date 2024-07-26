
# Challenge Qu

Developer Challenge - Word Finder



## Authors

- [@fedec93](https://www.github.com/fedec93)


## Features

- Console app with some settings to generate a random matrix and run the Word Finder tool.
- WordFinder and WordFinderParallel to compare single vs multi-thread lookup.
- Unit Tests
- Benchmarks

## Benchmarks

Using BenchmarkDotNet to compare the differences between single-thread and multi-thread lookup, with different inputs, to choose the right one depending on the needs.

The variable input are:
- Square Matrix size - It can be an irregular matrix too, but to simplify the benchmarks we use an square type
    - 8x8
    - 32x32
    - 64x64
- Number of words in the word-stream
    - 10
    - 100
    - 250

The results are:

| Method             | _matrixSize | _wordCount | Mean         | Error      | StdDev     | Allocated |
|------------------- |------------ |----------- |-------------:|-----------:|-----------:|----------:|
| WordFinder         | 8           | 10         |     1.870 us |  0.0215 us |  0.0201 us |   1.48 KB |
| WordFinderParallel | 8           | 10         |     4.849 us |  0.0760 us |  0.0674 us |  13.71 KB |
| WordFinder         | 8           | 100        |    15.111 us |  0.0710 us |  0.0630 us |   7.94 KB |
| WordFinderParallel | 8           | 100        |    18.159 us |  0.1276 us |  0.1131 us |  23.32 KB |
| WordFinder         | 8           | 250        |    36.768 us |  0.5684 us |  0.5317 us |  16.41 KB |
| WordFinderParallel | 8           | 250        |    34.124 us |  0.6672 us |  0.7416 us |  32.21 KB |
| WordFinder         | 32          | 10         |    24.334 us |  0.2365 us |  0.2097 us |   3.78 KB |
| WordFinderParallel | 32          | 10         |    13.336 us |  0.2400 us |  0.2245 us |  19.33 KB |
| WordFinder         | 32          | 100        |   207.620 us |  0.5167 us |  0.4580 us |  10.76 KB |
| WordFinderParallel | 32          | 100        |    59.525 us |  1.1084 us |  1.0368 us |  27.94 KB |
| WordFinder         | 32          | 250        |   521.088 us |  9.2111 us |  8.6161 us |  19.23 KB |
| WordFinderParallel | 32          | 250        |   121.634 us |  2.3715 us |  2.2183 us |  43.32 KB |
| WordFinder         | 64          | 10         |   111.771 us |  0.2613 us |  0.2444 us |  10.55 KB |
| WordFinderParallel | 64          | 10         |    59.501 us |  0.9758 us |  0.9128 us |  22.89 KB |
| WordFinder         | 64          | 100        |   779.883 us |  6.0212 us |  5.3377 us |  17.01 KB |
| WordFinderParallel | 64          | 100        |   163.489 us |  1.8852 us |  1.5742 us |   40.8 KB |
| WordFinder         | 64          | 250        | 1,849.471 us | 10.9335 us | 10.2272 us |  25.48 KB |
| WordFinderParallel | 64          | 250        |   286.914 us |  1.4856 us |  1.3170 us |  64.49 KB |

As the results show, when the matrix size and the word-stream are small, going for a multi-thread approach can be expensive and take more time compared to a single-thread approach. But once the Matrix and the word stream became larger, the processing time goes way faster in the multi-thread approach vs the single-thread.