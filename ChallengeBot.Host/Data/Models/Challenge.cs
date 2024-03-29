﻿namespace ChallengeBot.Host.Data.Models;

public class Challenge
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Stub { get; set; }
    public string Content { get; set; }
    public long CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Key { get; set; }
    public bool Active { get; set; }
}
