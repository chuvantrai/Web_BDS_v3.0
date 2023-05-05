﻿using WebBDS.Models;

namespace WebBDS.ResponseModels;

public class NewsDetailResponse
{
    public List<News>? Top3News { get; set; }
    public News? NewsDetail { get; set; }
}